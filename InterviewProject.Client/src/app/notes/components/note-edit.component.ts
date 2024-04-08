import {Component} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: "note-edit",
  templateUrl: "./note-edit.component.html",
  styleUrls: ["./note-edit.component.scss"]
})
export class NoteEditComponent {
  form: FormGroup = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
  });

  constructor() {
  }

  save() {

  }
}
