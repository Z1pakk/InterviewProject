import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {UsersComponent} from "./components/users.component";
import {MaterialUiModule} from "../material-ui.module";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {UserEditComponent} from "./components/user-edit.component";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [
    UsersComponent,
    UserEditComponent
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
        data: {title: "Edit User"},
      }
    ]),
    ReactiveFormsModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UserModule {}
