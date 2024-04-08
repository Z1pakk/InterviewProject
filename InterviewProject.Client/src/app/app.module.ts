import { HttpClientModule } from '@angular/common/http';
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MaterialUiModule} from "./material-ui.module";
import {ToolBarModule} from "./layout/toolbar/toolbar.module";
import {CommonModule} from "@angular/common";
import {ConfirmDialogComponent} from "./features/confirm-dialog/components/confirm-dialog.component";

@NgModule({
  declarations: [
    AppComponent,
    ConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    MaterialUiModule,

    ToolBarModule, FormsModule, ReactiveFormsModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
