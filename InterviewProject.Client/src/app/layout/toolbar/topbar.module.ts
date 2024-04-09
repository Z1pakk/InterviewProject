import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {TopbarComponent} from "./components/topbar.component";
import {MaterialUiModule} from "../../material-ui.module";
import {RouterModule} from "@angular/router";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [
    TopbarComponent
  ],
  exports: [
    TopbarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MaterialUiModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class TopbarModule {}
