import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Seller } from '@app/_models';
import { environment } from '@environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SellerService {

  constructor(private http: HttpClient) { }

  //get all sellers info
  getAllSellers(): Observable<Seller[]> {
    return this.http.get<Seller[]>(`${environment.apiUrl}/sellers`)
      .pipe(
        map((data: Seller[]) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }

  //get seller by Id
   getSellerByUserId(id: number):Observable<Seller> {
    return this.http.get(`${environment.apiUrl}/sellers/${id}`)
    .pipe(
        map((data: Seller) => {
            return data;
        }), catchError(error => {
            return throwError(error);
        })
    )
   }
  
  //get seller per advert
  sellerPerAdvert(advertId: number): Observable<Seller> {
    return this.http.get<Seller>(`${environment.apiUrl}/sellers/${advertId}/seller`)
      .pipe(
        map((data: Seller) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }

  //update seller details
  updateSeller(id: number, seller: Seller): Observable<Seller> {
    return this.http.put(`${environment.apiUrl}/sellers/${id}`, seller)
      .pipe(
        map((data: Seller) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      );
  }
}
