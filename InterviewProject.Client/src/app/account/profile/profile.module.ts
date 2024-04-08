import {NgModule} from "@angular/core";
import {ProfileComponent} from "./components/profile.component";
import {RouterModule} from "@angular/router";
import {MaterialUiModule} from "../../material-ui.module";

@NgModule({
  declarations: [
    ProfileComponent,

  ],
  imports :[
    MaterialUiModule,

    RouterModule.forChild([{
      path: "",
      component: ProfileComponent,
      data: { title: "My Profile" },
    }])
  ]
})
export class ProfileModule { }
