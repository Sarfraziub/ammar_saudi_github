import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { distinctUntilChanged, filter } from 'rxjs';

@Component({
    selector: 'app-breadcrump',
    templateUrl: './breadcrumb.component.html',
})
export class BreadcrumbComponent extends BaseComponent {

    public breadcrumbs: any[] = [];

    constructor(private router: Router,
        private activatedRoute: ActivatedRoute) {
        super();
        this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
    }

    ngOnInit() {
        this.router.events.pipe(
            filter((event) => event instanceof NavigationEnd),
            distinctUntilChanged(),
        ).subscribe(() => {
            this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
        });
    }

    buildBreadCrumb(route: ActivatedRoute, url: string = '', breadcrumbs: any[] = []): any[] {
        let label = route?.routeConfig?.data?.title || '';
        let path = route?.routeConfig?.path || '';
        const isBase = route?.routeConfig?.data?.isBase || '';
        const rootIcon = route?.routeConfig?.data?.rootIcon || '';
        const isBreadcrumbHidden = route?.routeConfig?.data?.isBreadcrumbHidden;
        const lastRoutePart = path.split('/').pop();
        const isDynamicRoute = lastRoutePart.startsWith(':');
        if (isDynamicRoute && !!route.snapshot) {
            const paramName = lastRoutePart.split(':')[1];
            path = path.replace(lastRoutePart, route.snapshot.params[paramName]);
        }

        let nextUrl = path ? `${url}/${path}` : url;
        route.params.subscribe(value => {
            Object.keys(value).forEach(key => {
                nextUrl = nextUrl.replace(`:${key}`, value[key]);
            });
        });

        let breadcrumb = {
            label: label,
            url: nextUrl,
            isBase: isBase,
            rootIcon: rootIcon,
            isBreadcrumbHidden: isBreadcrumbHidden,
        };

        const newBreadcrumbs = breadcrumb.label || isBreadcrumbHidden ? [...breadcrumbs, breadcrumb] : [...breadcrumbs];
        if (route.firstChild) {
            return this.buildBreadCrumb(route.firstChild, nextUrl, newBreadcrumbs);
        }
        if (newBreadcrumbs?.find(f => f.isBreadcrumbHidden)) {
            return [];
        }
        return newBreadcrumbs;
    }
}
