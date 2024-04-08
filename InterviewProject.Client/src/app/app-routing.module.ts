import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: "login",  loadChildren: () => import("./account/login/login.module").then(m => m.LoginModule) },
  { path: "profile",  loadChildren: () => import("./account/profile/profile.module").then(m => m.ProfileModule) },
  { path: "users",  loadChildren: () => import("./users/user.module").then(m => m.UserModule) },
  { path: "notes",  loadChildren: () => import("./notes/note.module").then(m => m.NoteModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
