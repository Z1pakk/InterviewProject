import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MaterialUiModule} from "./material-ui.module";
import {TopbarModule} from "./layout/toolbar/topbar.module";
import {CommonModule} from "@angular/common";
import {ConfirmDialogComponent} from "./features/confirm-dialog/components/confirm-dialog.component";
import {AccountService} from "./account/services/account.service";
import {HttpStatusesInterceptor} from "./modules/security/http-statuses.interceptor";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NoteService} from "./notes/services/note.service";

@NgModule({
  declarations: [
    AppComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    MaterialUiModule,

    TopbarModule, FormsModule, ReactiveFormsModule,

    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      closeButton: true,
      progressBar: true
    }),
  ],
  providers: [
    provideAnimationsAsync(),
    AccountService,

    {provide: HTTP_INTERCEPTORS, useClass: HttpStatusesInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
