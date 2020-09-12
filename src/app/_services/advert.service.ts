import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Advert } from '@app/_models/advert';
import { environment } from '@environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ProvinceModel } from '@app/_models/province';
import { CityModel } from '@app/_models/city';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient,private messageService: MessageService) { }

  //get all available advert
  getAllAdvert(): Observable<Advert[]> {
    return this.http.get<Advert[]>(`${environment.apiUrl}/adverts`)
      .pipe(
        tap(_ => this.log('fetched adverts')),
        catchError(this.handleError<Advert[]>('getAdvert', []))
      );
  }

  //get advert by Id 
  getAdvertById(id: number): Observable<Advert> {
    return this.http.get<Advert>(`${environment.apiUrl}/adverts/${id}`)
      .pipe(
        tap(_ => this.log(`fetched advert id=${id}`)),
        catchError(this.handleError<Advert>(`getAdvert id=${id}`))
      );
  }

  //get advert by user Id
  getAdvertsByUserId(userId: number): Observable<Advert[]> {
    if (userId === 0) {
      of(this.initializeAdvert());
    }
    return this.http.post<Advert[]>(`${environment.apiUrl}/adverts`, { userId })
      .pipe(map(advert => {
        return advert;
      }));
  }

  //add or post new advert
  addAdvert(advert: Advert) {
    return this.http.post(`${environment.apiUrl}/adverts/addAdvert`, advert)
      .pipe(
        tap(data => this.log('addAdvert: ' + JSON.stringify(data))),
        catchError(this.handleError<Advert>('addAdvert'))
      );
  }

  //update advert
  updateAdvert(id: number, advert: Advert) {
    return this.http.put(`${environment.apiUrl}/adverts/${id}`, advert)
      .pipe(
        //return advert and update
        map(() => advert,
          catchError(this.handleError<Advert>('updateAdvert')))
      );
  }

  //remove advert by id from the list
  deleteAdvert(id: number) {
    return this.http.delete(`${environment.apiUrl}/adverts/${id}`)
      .pipe(
        tap(_ => this.log(`deleted advert id=${id}`)),
        catchError(this.handleError<Advert>('deleteAdvert'))
      );
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
    return this.http.post<CityModel[]>(`${environment.apiUrl}/address/city`, { provinceId })
      .pipe(map(city => {
        return city;
      }));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log an advert service message with the MessageService */
  private log(message: string) {
    this.messageService.add(`AdvertService: ${message}`);
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
