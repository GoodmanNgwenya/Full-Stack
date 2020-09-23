import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { AlertComponent } from './_components';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmationDialogComponent } from './shared/confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        MatDialogModule,
        ReactiveFormsModule,
        HttpClientModule,
        FormsModule,
        NgxPaginationModule,
        Ng2SearchPipeModule,
        AppRoutingModule,
        JwPaginationModule
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        AdvertComponent,
        RegisterComponent,
        HomesForSaleComponent,
        AdvertDetailsComponent,
        AlertComponent,
        ConfirmationDialogComponent],
        entryComponents: [
          ConfirmationDialogComponent
        ],
        
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
        // provider used to create fake backend
        //fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }