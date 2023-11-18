import { Component, ElementRef, EventEmitter, Input, NgZone, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { GoogleMap } from '@angular/google-maps';
import { MapGeocoder } from '@angular/google-maps';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-google-map',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./google-map.component.scss'],
    templateUrl: './google-map.html',
})
export class GoogleMapComponent {

    @Output() onSetMarker = new EventEmitter<any>();
    @Output() onClickMarker = new EventEmitter<any>();

    @Input() selectedMarkers = [];
    private _selectedMarker: any = null;
    @Input() get selectedMarker() {
        return this._selectedMarker;
    }

    set selectedMarker(item: any) {
        this.markers = [];
        if (item) {
            this.addMarker(item);
        } else {
            this.markers = [];
        }
    }


    @ViewChild('search')
    public searchElementRef!: ElementRef;
    @ViewChild(GoogleMap)
    public map!: GoogleMap;

    zoom = 12;
    center!: google.maps.LatLngLiteral;
    options: google.maps.MapOptions = {
        zoomControl: true,
        scrollwheel: false,
        disableDefaultUI: true,
        fullscreenControl: true,
        disableDoubleClickZoom: true,
        mapTypeId: 'hybrid',
    };
    latitudeMap!: any;
    longitudeMap!: any;


    markers = [];

    constructor(private ngZone: NgZone, private geocoder: MapGeocoder) {
    }

    ngAfterViewInit(): void {
        // Binding autocomplete to search input control
        let autocomplete = new google.maps.places.Autocomplete(
            this.searchElementRef.nativeElement
        );
        // Align search box to center
        this.map.controls[google.maps.ControlPosition.TOP_CENTER].push(
            this.searchElementRef.nativeElement
        );
        autocomplete.addListener('place_changed', () => {
            this.ngZone.run(() => {
                //get the place result
                let place: google.maps.places.PlaceResult = autocomplete.getPlace();

                //verify result
                if (place.geometry === undefined || place.geometry === null) {
                    return;
                }

                console.log({ place }, place.geometry.location?.lat());

                //set latitude, longitude and zoom
                this.latitudeMap = place.geometry.location?.lat();
                this.longitudeMap = place.geometry.location?.lng();
                this.center = {
                    lat: this.latitudeMap,
                    lng: this.longitudeMap,
                };
            });
        });
    }

    ngOnInit() {
        if(this.selectedMarkers?.length) 
            this.markers = this.selectedMarkers
        // Kaaba is the default position
        this.center = {
            lat: 21.4225,
            lng: 39.8262
        }
    }

    eventHandler(event: any, name: string) {
        console.log(event, name);

        // Add marker on double click event
        if (name === 'mapDblclick') {
            this.dropMarker(event)
        }
    }

    addMarker(event: any) {

        const image = "assets/mosque.png";

        let markers = [];

        markers.push({
            position: {
                lat: event.lat,
                lng: event.lng,
            },
            options: {
                icon: image,
                animation: google.maps.Animation.DROP,
                id: event.id
            },
        });

        this.markers = [...markers];

        navigator.geolocation.getCurrentPosition(() => {
            this.center = {
                lat: event.lat,
                lng: event.lng,
            };
        });
    }

    dropMarker(event: any) {

        const image = "assets/mosque.png";

        let markers = [];

        const latlng = {
            lat: parseFloat(event.latLng.lat()),
            lng: parseFloat(event.latLng.lng()),
        };


        this.geocoder.geocode({
            location: latlng
        }).subscribe(({ results }) => {
            markers.push({
                position: {
                    lat: event.latLng.lat(),
                    lng: event.latLng.lng(),
                },
                options: {
                    icon: image,
                    animation: google.maps.Animation.DROP,
                },
            });
            this.markers = [...markers];

            this.onSetMarker.emit({ event: event, address: results });
        });
    }

    openInfo(event, marker) {
       this.onClickMarker.emit(marker.options.id)
    }

}


