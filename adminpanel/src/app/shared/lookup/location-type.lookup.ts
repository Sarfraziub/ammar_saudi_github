import { LocationType } from '../enum';


export class LocationTypeLookup {
    static get getLookup() {
        return [
            { text: 'يرجى الاختيار', id: '' },
            { text: 'مسجد', id: LocationType.Mosque },
            { text: 'دار أيتام', id: LocationType.Orphanage },
        ];
    }

    static getById(id) {
        return this.getLookup.find(f => f.id === id)?.text;
    }
}
