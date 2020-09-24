import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Advert } from '@app/_models/advert';
import { environment } from '@environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ProvinceModel } from '@app/_models/province';
import { CityModel } from '@app/_models/city';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient) { }

  //get all available advert
  getAllAdvert(): Observable<Advert[]> {
    return this.http.get<Advert[]>(`${environment.apiUrl}/adverts`)
      .pipe(
        map((data: Advert[]) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }

   /* GET advert whose name contains search term */
   searchAdvert(province: string): Observable<Advert[]> {
    if (!province.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Advert[]>(`${environment.apiUrl}/adverts/search?province=${province}`)
      .pipe(
        map((data: Advert[]) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }

  //get advert by Id 
  getAdvertById(id: number): Observable<Advert> {
    return this.http.get<Advert>(`${environment.apiUrl}/adverts/${id}`)
      .pipe(
        map((data: Advert) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }

  //get advert by user Id
  getAdvertsByUserId(userId: number): Observable<Advert[]> {
    if (userId === 0) {
      of(this.initializeAdvert());
    }
    return this.http.get<Advert[]>(`${environment.apiUrl}/adverts/${userId}/adverts`)
      .pipe(
        map((data: Advert[]) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }

  //add or post new advert
  addAdvert(advert: Advert): Observable<Advert> {
    return this.http.post(`${environment.apiUrl}/adverts`, advert)
      .pipe(
        map((data: Advert) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      );
  }


  //update advert
  updateAdvert(id: number, advert: Advert): Observable<Advert> {
    return this.http.put(`${environment.apiUrl}/adverts/${id}`, advert)
      .pipe(
        map((data: Advert) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        }));
  }

  //remove advert by id from the list
  deleteAdvert(id: number) {
    return this.http.delete(`${environment.apiUrl}/adverts/${id}`)
      .pipe(
        map((data: Advert) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        }));
  }

  //get all available province
  getAllProvince(): Observable<ProvinceModel[]> {
    return this.http.get<ProvinceModel[]>(`${environment.apiUrl}/address`)
      .pipe(map(province => {
        return province;
      }))
  }

  //get all cities based on selected province
  getCity(provinceId: number): Observable<CityModel[]> {
    if (provinceId === 0) {
      of(this.initializeCity());
    }
    return this.http.get<CityModel[]>(`${environment.apiUrl}/address/${provinceId}/cities`)
      .pipe(
        map((data: CityModel[]) => {
          return data;
        }), catchError(error => {
          return throwError(error);
        })
      )
  }


  private initializeAdvert(): Advert {
    // Return an initialized object
    return {
      id: 0,
      advertHeadlineText: null,
      releaseDate: null,
      province: null,
      city: null,
      price: null,
      advertDetails: null,
      advertStatus: null,
      userId: 0,
      imageUrl: null
    };
  }

  private initializeCity(): CityModel {
    // Return an initialized object
    return {
      id: 0,
      city: null,
      provinceId: 0
    };
  }

}
