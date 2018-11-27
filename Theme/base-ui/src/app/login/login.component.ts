import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AlertService } from '../core/auth/alert.service';
import { AuthenticationService } from '../core/auth/authentication.service';

@Component({
    selector: 'login',
    styleUrls: ['./login.style.scss'],
    templateUrl: './login.template.html',
    encapsulation: ViewEncapsulation.None,
    host: {
        class: 'login-page app'
    }
})
export class Login implements OnInit {
    model: any = {};
    loading = false;
    returnUrl: string;
    loginResult: any;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService) { }

    ngOnInit() {
        // reset login status
        this.authenticationService.logout().then(res => {
            if (!res.Success)
                alert(res.Messages[0]);
        }).catch(err => this.handleError(err));

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    login() {
        this.loading = true;

        this.authenticationService.login(this.model.username, this.model.password)
            .subscribe(
            data => {
                this.loginResult = { success: true, Messages: ["Giriş başarılı."] };
                this.router.navigate([this.returnUrl]);
            },
            response => {
                var json = response.json();
                var error = json.error;
                this.loginResult = { success: false, Messages: ["Kullanıcı adı veya Şifre hatalı."] };
                this.loading = false;
            });
    }

    private handleError(error: any): void {
        if (error.status == 401)
            this.router.navigate(['/login']);
        else
            this.router.navigate(['/error']);
    }

}