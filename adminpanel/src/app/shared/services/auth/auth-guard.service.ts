import { Injectable } from '@angular/core';
import { Router, CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, CanDeactivate, CanLoad, UrlTree, Route, UrlSegment } from '@angular/router';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild, CanDeactivate<unknown>, CanLoad {


    constructor(private tokenStorageService: TokenStorageService, private router: Router, private storageService: StorageService) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        let url: string = state.url;
        return this.checkUserLogin(next, url);
    }
    canActivateChild(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.canActivate(next, state);
    }
    canDeactivate(
        component: unknown,
        currentRoute: ActivatedRouteSnapshot,
        currentState: RouterStateSnapshot,
        nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return true;
    }
    canLoad(
        route: Route,
        segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
        return true;
    }

    checkUserLogin(route: ActivatedRouteSnapshot, url: any): boolean {
        if (!route.data.role && !route.data.roles) {
            return true;
        }
        const token = this.tokenStorageService.getToken();

        if (token) {
            return true;
        }
        this.storageService.secureStorage.setItem('redirect-url', url);
        this.router.navigate(['/pages/home']);
        return false;
    }
}