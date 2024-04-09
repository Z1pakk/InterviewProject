import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  canAuthenticatedActivate,
  canAuthenticatedActivateChild
} from "./modules/security/security.guard";

const routes: Routes = [
  { path: "", redirectTo: "profile", pathMatch: "full" },
  { path: "login",  loadChildren: () => import("./account/login/login.module").then(m => m.LoginModule) },
  { path: "profile", canActivate: [canAuthenticatedActivate], canActivateChild: [canAuthenticatedActivateChild],  loadChildren: () => import("./account/profile/profile.module").then(m => m.ProfileModule) },
  { path: "users", canActivate: [canAuthenticatedActivate], canActivateChild: [canAuthenticatedActivateChild],  loadChildren: () => import("./users/user.module").then(m => m.UserModule) },
  { path: "notes", canActivate: [canAuthenticatedActivate], canActivateChild: [canAuthenticatedActivateChild],  loadChildren: () => import("./notes/note.module").then(m => m.NoteModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
