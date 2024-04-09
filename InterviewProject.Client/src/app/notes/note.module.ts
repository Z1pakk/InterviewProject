import {NgModule} from "@angular/core";
import {NotesComponent} from "./components/notes.component";
import {MaterialUiModule} from "../material-ui.module";
import {CommonModule} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {NoteEditComponent} from "./components/note-edit.component";
import {NoteService} from "./services/note.service";

@NgModule({
  declarations: [
    NotesComponent,
    NoteEditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
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
      {
        path: "create",
        component: NoteEditComponent,
        data: { title: "Create Note" },
      },
    ]),
    MaterialUiModule,
    ReactiveFormsModule
  ],
  providers: [
    NoteService
  ]
})
export class NoteModule{}
