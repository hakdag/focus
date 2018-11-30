import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Token } from './token';
 
@Injectable()
export class AuthGuard implements CanActivate {
 
    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
        if (this.checkToken())
            return true;

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }

    checkToken(): boolean{
        if (localStorage.getItem('token') == undefined)
            return false;

        var token: Token = JSON.parse(localStorage.getItem('token'));     

        if (token == undefined || token[".expires"] == undefined)
            return false;

        token.expires = new Date(token[".expires"]);
        return token.expires > new Date();
    }
}