import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'; //HttpClient, ?
import { ApiUrlInterceptor } from './interceptors/api-url.interceptor';
import { EnvironmentService, GroupService,  } from './services';
import { UsersComponent, GroupsComponent } from './components';

@NgModule({
  declarations: [
    AppComponent,
    GroupsComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    EnvironmentService,
    GroupService,
    { provide: HTTP_INTERCEPTORS, useClass: ApiUrlInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
