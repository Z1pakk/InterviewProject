import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {LoginComponent} from "./components/login.component";
import {RouterModule} from "@angular/router";
import {MaterialUiModule} from "../../material-ui.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AccountService} from "../services/account.service";

@NgModule({
  declarations: [
      LoginComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialUiModule,

    RouterModule.forChild([{
      path: "",
      component: LoginComponent,
      data: { title: "Login Page" },
    }])
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LoginModule { }
