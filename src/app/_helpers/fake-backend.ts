import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { delay, mergeMap, materialize, dematerialize } from 'rxjs/operators';

import { User, ProvinceModel, CityModel } from '@app/_models';
import { Advert } from '@app/_models/advert';

const users: User[] = [{ id: 1, username: 'test@gmail.com', password: '12345678', firstName: 'Test', lastName: 'Test', role: 'User' },
{ id: 2, username: 'goodman@gmail.com', password: '12345678', firstName: 'Goodman', lastName: 'Ngwenya', role: 'Admin' }];

let adverts: Advert[] =
    [{ id: 1, advertHeadlineText: 'Testing one', province: 'Gauteng', city: 'Sandton', advertDetails: '', price: 18000000.05, releaseDate: '', imageUrl: '', advertStatus: '', userId: 1 },
    { id: 2, advertHeadlineText: 'Testing two', province: 'Mpumalanga', city: 'Witbank', advertDetails: '', price: 355500.47, releaseDate: '', imageUrl: '', advertStatus: '', userId: 1 },
    { id: 3, advertHeadlineText: 'Testing Three', province: 'Limpopo', city: 'Polokwane', advertDetails: '', price: 374500.47, releaseDate: '', imageUrl: '', advertStatus: '', userId: 0 }
    ]

let provinces: ProvinceModel[] =
    [{ id: 1, province: 'Mpumalanga' }, { id: 2, province: 'Gauteng' }, { id: 3, province: 'Western Cape' }, { id: 4, province: 'North West' }, { id: 5, province: 'Free State' }]

let cities: CityModel[] =
    [{ id: 1, city: 'Nelspruit', provinceId: 1 }, { id: 2, city: 'Witbank', provinceId: 1 }, { id: 3, city: 'Ermelo', provinceId: 1 },
    { id: 4, city: 'Johannesburg', provinceId: 2 }, { id: 5, city: 'Pretoria', provinceId: 2 }, { id: 6, city: 'Soweto', provinceId: 2 },
    { id: 7, city: 'Cape Town', provinceId: 3 }, { id: 8, city: 'Stellenbosch', provinceId: 3 }, { id: 9, city: 'Knysna', provinceId: 3 },
    { id: 10, city: 'Mahikeng', provinceId: 4 }, { id: 11, city: 'Marikana', provinceId: 4 },
    { id: 12, city: 'Bloemfontein', provinceId: 5 }, { id: 13, city: 'Welkom', provinceId: 5 },
    ]


@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const { url, method, headers, body } = request;

        // wrap in delayed observable to simulate server api call
        return of(null)
            .pipe(mergeMap(handleRoute))
            .pipe(materialize()) // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
            .pipe(delay(500))
            .pipe(dematerialize());

        function handleRoute() {
            switch (true) {
                //users
                case url.endsWith('/users/authenticate') && method === 'POST':
                    return authenticate();
                case url.endsWith('/users/register') && method === 'POST':
                    return register();
                case url.endsWith('/users') && method === 'GET':
                    return getUsers();
                case url.match(/\/users\/\d+$/) && method === 'GET':
                    return getUserById();
                case url.match(/\/users\/\d+$/) && method === 'PUT':
                    return updateUser();

                //province and city
                case url.endsWith('/address') && method === 'GET':
                    return getProvinces();
                case url.endsWith('/address/city') && method === 'POST':
                    return getCities();

                //advert
                case url.endsWith('/adverts/addAdvert') && method === 'POST':
                    return createAdvert();
                case url.endsWith('/adverts') && method === 'GET':
                    return getAdverts();
                case url.match(/\/adverts\/\d+$/) && method === 'GET':
                    return getAdvertById();
                case url.endsWith('/adverts') && method === 'POST':
                    return getAdvertsById();
                case url.match(/\/adverts\/\d+$/) && method === 'PUT':
                    return updateAdvert();
                case url.match(/\/adverts\/\d+$/) && method === 'DELETE':
                    return deleteAdvert();
                default:
                    // pass through any requests not handled above
                    return next.handle(request);
            }
        }

        // route functions

        function authenticate() {
            const { username, password } = body;
            const user = users.find(x => x.username === username && x.password === password);
            if (!user) return error('Username or password is incorrect');
            return ok({
                id: user.id,
                username: user.username,
                firstName: user.firstName,
                lastName: user.lastName,
                role: user.role,
                token: 'fake-jwt-token'
            })
        }

        function register() {
            const user = body
            user.role = "User";

            if (users.find(x => x.username === user.username)) {
                return error('Username "' + user.username + '" is already taken')
            }

            user.id = users.length ? Math.max(...users.map(x => x.id)) + 1 : 1;
            users.push(user);
            localStorage.setItem('users', JSON.stringify(users));
            return ok();
        }

        function getUsers() {
            if (!isLoggedIn()) return unauthorized();
            return ok(users);
        }

        function getUserById() {
            if (!isLoggedIn()) return unauthorized();

            const user = users.find(x => x.id == idFromUrl());
            return ok(user);
        }

        function updateUser() {
            let params = body;
            let user = users.find(x => x.id === idFromUrl());

            if (params.OldPassword == user.password) {

                return error("Password alread exist, try another one");
            }

            // only update password if entered
            if (!params.password) {
                delete params.password;
            }

            // update and save user
            Object.assign(user, params);
            localStorage.setItem('users', JSON.stringify(users));

            return ok();
        }

        //get province and city
        function getProvinces() {
            return ok(provinces);
        }
        function getCities() {
            const { provinceId } = body;
            const listCities = cities.filter(x => x.provinceId === provinceId);
            return ok(listCities);
        }

        /** 
         * crud operation for advert details
         */

        //retrieve all advert
        function getAdverts() {
            return ok(adverts);
        }

        //retrieve advert by Id
        function getAdvertById() {
            const advert = adverts.find(x => x.id == idFromUrl());
            return ok(advert);
        }

        //retrieve all advert posted by a specific user by ID
        function getAdvertsById() {
            const { userId } = body;

            if (!isLoggedIn()) return unauthorized();
            const advert = adverts.filter(x => x.userId === userId);
            return ok(advert);
        }

        //create advert
        function createAdvert() {
            const advert = body

            // advert.id = adverts.length ? Math.max(...adverts.map(x => x.id)) + 1 : 1;
            advert.id = newAdvertId();
            adverts.push(advert);
            localStorage.setItem('adverts', JSON.stringify(adverts));
            return ok();
        }

        //update advert details
        function updateAdvert() {
            let params = body;
            let advert = adverts.find(x => x.id === idFromUrl());

            // update and save advert
            Object.assign(advert, params);
            localStorage.setItem('adverts', JSON.stringify(adverts));

            return ok();
        }

        //delete advert
        function deleteAdvert() {
            adverts = adverts.filter(x => x.id !== idFromUrl());
            localStorage.setItem('adverts', JSON.stringify(adverts));
            return ok();
        }
        
        //seller crud operation
        function updateSeller() {
            let params = body;
            let user = users.find(x => x.id === idFromUrl());

            // update and save seller
            Object.assign(user, params);
            localStorage.setItem('users', JSON.stringify(users));

            return ok();
        }


        // helper functions

        function ok(body?) {
            return of(new HttpResponse({ status: 200, body }))
        }

        function error(message) {
            return throwError({ error: { message } });
        }

        function unauthorized() {
            return throwError({ status: 401, error: { message: 'Unauthorised' } });
        }

        function isLoggedIn() {
            return headers.get('Authorization') === 'Bearer fake-jwt-token';
        }
        function idFromUrl() {
            const urlParts = url.split('/');
            return parseInt(urlParts[urlParts.length - 1]);
        }
        function newAdvertId() {
            return adverts.length ? Math.max(...adverts.map(x => x.id)) + 1 : 1;
        }
    }
}

export let fakeBackendProvider = {
    // use fake backend in place of Http service for backend-less development
    provide: HTTP_INTERCEPTORS,
    useClass: FakeBackendInterceptor,
    multi: true
};