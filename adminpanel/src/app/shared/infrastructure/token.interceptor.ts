import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';

// import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { AppModule } from 'app/app.module';
import { throwError, BehaviorSubject, Observable } from 'rxjs';
import { catchError, filter, finalize, switchMap, take, tap } from 'rxjs/operators';
import { AuthService } from '../services';
import { TokenStorageService } from '../services/auth/token-storage.service';
import { LoadingService } from '../services/loading.service';


@Injectable({
    providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor {
    private _tokenStorageService: TokenStorageService;
    private loadingService: LoadingService;

    private totalRequests = 0;

    constructor() {
        this._tokenStorageService = AppModule.injector.get(TokenStorageService);
        this.loadingService = AppModule.injector.get(LoadingService);

    }

    setHeader(request, token, responseType) {
        if (token) {
            return request.clone({
                setHeaders: {
                    'Accept': "application/json",
                    'Content-Type': 'application/json',
                    'Accept-Language': 'en',
                    'Authorization': token ? `Bearer ${token.accessToken}` : '',
                }, responseType: responseType
            });
        } else {
            return request.clone({
                setHeaders: {
                    'Accept': "application/json",
                    'Content-Type': 'application/json',
                    'Accept-Language': 'en',
                }, responseType: responseType
            });
        }
    }

    setAttachementHeader(request, token, responseType) {
        return request.clone({
            setHeaders: {
                'Accept': "text/plain",
                'Accept-Language': 'en',
                'Authorization': token ? `Bearer ${token.accessToken}` : '',
            }, responseType: responseType
        });
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<Object>> {
        if (!request.url.includes('RefreshTokenSync')) {
            this.totalRequests++;
            this.loadingService.setLoading(true);
        }

        if (request.responseType == 'attachement' as 'json') {
            request = this.setAttachementHeader(request, this._tokenStorageService.getToken(), 'json');
        } else{
            
            request = this.setHeader(request, this._tokenStorageService.getToken(), request.responseType);
        }


        
        return next.handle(request).pipe(catchError(error => {
            if (error instanceof HttpErrorResponse && !request.url.includes('Accounts/LoginSync') && error.status === 401) {
                this._tokenStorageService.tokenSignOut();
                window.location.reload();
            } else if (error.status === 404) {
                return throwError("لا يوجد بيانات");
            }

            return throwError(error);
        }),
            finalize(() => {
                if (!request.url.includes('RefreshTokenSync')) {
                    this.totalRequests--;
                    if (this.totalRequests === 0) {
                        this.loadingService.setLoading(false);
                    }
                }

            }));
    }


    onHandleRequest(next, request) {
        return next.handle(request)
            .pipe(
                tap(
                    // Succeeds when there is a response; ignore other events
                    event => {

                    },
                    // Operation failed; error is an HttpErrorResponse
                    error => {
                        if (error.status === 401) {
                            this._tokenStorageService.tokenSignOut();
                            window.location.reload();
                        } else {
                            return throwError(error.statusText);
                        }
                    }
                ),
                // Log when response observable either completes or errors
                finalize(() => {
                    this.totalRequests--;
                    if (this.totalRequests === 0) {
                        this.loadingService.setLoading(false);
                    }
                })
            );
    }
}