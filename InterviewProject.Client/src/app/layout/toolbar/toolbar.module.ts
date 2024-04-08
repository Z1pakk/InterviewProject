import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {ToolbarComponent} from "./components/toolbar.component";
import {MaterialUiModule} from "../../material-ui.module";
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [
    ToolbarComponent
  ],
  exports: [
    ToolbarComponent
  ],
  imports: [
    RouterModule,
    MaterialUiModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ToolBarModule {}
