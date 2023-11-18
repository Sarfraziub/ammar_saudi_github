import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
export class UploadService {

    private subject = new Subject<any>();
    private userInformation = new Subject<any>();

    constructor(private http: HttpClient) {

    }

    public upload(file): any {

        const formData: FormData = new FormData();

        formData.append('file', file, file.name);

        return this.http.post(`${environment.api}/Files/Upload`, formData, { responseType: 'attachement' as 'json' })
            .pipe(first())
    }

    public getPhotoUrl(id): any {
        return this.http.get(`${environment.api}/Files/GetPhotoUrl?Id=${id}`)
            .pipe(first())
    }

    setUserInformation(data: any) {
        this.userInformation.next(data);
    }

    onChangeUserInformation(): Observable<any> {
        return this.userInformation.asObservable();
    }
}