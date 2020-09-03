import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { delay, mergeMap, materialize, dematerialize } from 'rxjs/operators';

import { User } from '@app/_models';
import { Advert } from '@app/_models/advert';

const users: User[] = [{ id: 1, username: 'test@gmail.com', password: '12345678', firstName: 'Test', lastName: 'Test', role: 'User' },
{ id: 2, username: 'goodman@gmail.com', password: '12345678', firstName: 'Goodman', lastName: 'Ngwenya', role: 'Admin' }];

let adverts: Advert[] =
    [{ id: 1, advertHeadlineText: 'Testing one', province: 'Gauteng', city: 'Sandton', advertDetails: '', price: 18000000.05, releaseDate: '', imageUrl: '', advertStatus: '', userId: 1 },
    { id: 2, advertHeadlineText: 'Testing two', province: 'Mpumalanga', city: 'Witbank', advertDetails: '', price: 355500.47, releaseDate: '', imageUrl: '', advertStatus: '', userId: 1 },
    { id: 3, advertHeadlineText: 'Testing Three', province: 'Limpopo', city: 'Polokwane', advertDetails: '', price: 374500.47, releaseDate: '', imageUrl: '', advertStatus: '', userId: 0 }
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

                //advert
                case url.endsWith('/adverts/create') && method === 'POST':
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

        /** 
         * crud operation for advert details
         */

        //retrieve all advert
        function getAdverts() {
            if (!isLoggedIn()) return unauthorized();
            return ok(adverts);
        }

        //retrieve advert by Id
        function getAdvertById() {
            if (!isLoggedIn()) return unauthorized();
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

            advert.id = adverts.length ? Math.max(...users.map(x => x.id)) + 1 : 1;
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
    }
}

export let fakeBackendProvider = {
    // use fake backend in place of Http service for backend-less development
    provide: HTTP_INTERCEPTORS,
    useClass: FakeBackendInterceptor,
    multi: true
};