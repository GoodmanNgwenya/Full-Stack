import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// used to create fake backend
//import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AdvertComponent } from './pages/advert/advert.component';
import { RegisterComponent } from './register/register.component';
import { HomesForSaleComponent } from './pages/homes-for-sale/homes-for-sale.component';
import { AdvertDetailsComponent } from './pages/advert-details/advert-details.component';
import { JwPaginationModule } from 'jw-angular-pagination';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        JwPaginationModule],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        AdvertComponent,
        RegisterComponent,
        HomesForSaleComponent,
        AdvertDetailsComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

        // provider used to create fake backend
        //fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }