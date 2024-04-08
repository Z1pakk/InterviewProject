import {NgModule} from "@angular/core";
import {NotesComponent} from "./components/notes.component";
import {MaterialUiModule} from "../material-ui.module";
import {CommonModule} from "@angular/common";
import {ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {NoteEditComponent} from "./components/note-edit.component";

@NgModule({
  declarations: [
    NotesComponent,
    NoteEditComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: "",
        component: NotesComponent,
        data: { title: "Notes" },
      },
      {
        path: "edit/:id",
        component: NoteEditComponent,
        data: { title: "Edit Note" },
      },
    ]),
    MaterialUiModule,
    ReactiveFormsModule
  ]
})
export class NoteModule{}
