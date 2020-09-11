import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';
import { AdvertComponent } from './pages/advert/advert.component';
import { RegisterComponent } from './register';
import { AdvertEditGuard } from './pages/advert-edit.guard';
import { CreateEditAdvertComponent, AdvertDetailsComponent, ManageAccountComponent } from './pages';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


const routes: Routes = [
  //{ path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'advert', component: AdvertComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }

  // otherwise redirect to home
  //{ path: '**', redirectTo: '' }
  // { path: '', redirectTo: '', pathMatch: 'full' }

];
const childroutes: Routes = [
  { path: 'advert', component: AdvertComponent, canActivate: [AuthGuard] },
  { path: 'editAdvert', component: CreateEditAdvertComponent },
  { path: 'editAdvert/:id/edit', component: CreateEditAdvertComponent },
  { path: 'viewAdvert/:id/view', component: AdvertDetailsComponent },
  { path: 'manageUser', component: ManageAccountComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
  RouterModule.forChild(childroutes),
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  declarations: [
    CreateEditAdvertComponent,
    ManageAccountComponent
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
