import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';
import { AdvertComponent } from './pages/advert/advert.component';
import { RegisterComponent } from './register';


const routes: Routes = [
    //{ path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'advert', component: AdvertComponent, canActivate: [AuthGuard]},
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent },
    { path: 'register', component:RegisterComponent},
    { path: '', redirectTo: 'home', pathMatch: 'full' }

    // otherwise redirect to home
    //{ path: '**', redirectTo: '' }
   // { path: '', redirectTo: '', pathMatch: 'full' }
    
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
