import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/environment-model';

@Injectable({ providedIn: 'root' })
export class EnvironmentService {
    getEnvironment(): IEnvironment {
        return environment;
    }
}
