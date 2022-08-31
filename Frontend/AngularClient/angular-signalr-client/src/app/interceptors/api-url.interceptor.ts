import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentService } from '../services/environment.service';

@Injectable({ providedIn: 'root' })
export class ApiUrlInterceptor implements HttpInterceptor {
    constructor(private readonly environmentService: EnvironmentService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.url.includes('assets')) {
            return next.handle(req);
        }

        const env = this.environmentService.getEnvironment();
        const modified = req.clone({
            url: env.apiUrlPrefix + req.url,
        });

        return next.handle(modified);
    }
}
