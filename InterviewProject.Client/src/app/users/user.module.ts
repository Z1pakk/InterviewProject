import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {UsersComponent} from "./components/users.component";
import {MaterialUiModule} from "../material-ui.module";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {UserEditComponent} from "./components/user-edit.component";
import {CommonModule} from "@angular/common";
import {UserService} from "./services/user.service";
import {canAdminActivate} from "../modules/security/security-admin.guard";
import {MatchPasswordValidatorDirective} from "../modules/directives/match-password.validator";

@NgModule({
  declarations: [
    UsersComponent,
    UserEditComponent,
    MatchPasswordValidatorDirective
  ],
  imports: [
    CommonModule,
    MaterialUiModule,
    FormsModule,
    RouterModule.forChild([
      {
        path: "",
        component: UsersComponent,
        data: {title: "Users"},
      },
      {
        path: "edit/:id",
        component: UserEditComponent,
        canActivate: [canAdminActivate],
        data: {title: "Edit User"},
      },
      {
        path: "create",
        component: UserEditComponent,
        canActivate: [canAdminActivate],
        data: {title: "Create User"},
      }
    ]),
    ReactiveFormsModule
  ],
  providers: [
    UserService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UserModule {}
