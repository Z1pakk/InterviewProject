import {Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {getFormFieldError} from "../../utils/get-from-error";
import {ValidationPatterns} from "../../features/validation/validation-patterns";
import {IUser} from "../../users/interfaces/user";
import {INote} from "../interfaces/note";
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../../users/services/user.service";
import {ToastrService} from "ngx-toastr";
import {deepCopy} from "../../utils/deepCopy";
import CryptoES from "crypto-es";
import {NoteService} from "../services/note.service";

@Component({
  selector: "note-edit",
  templateUrl: "./note-edit.component.html",
  styleUrls: ["./note-edit.component.scss"]
})
export class NoteEditComponent {
  protected readonly getFormFieldError = getFormFieldError;

  public note: INote = <INote>{};

  public isCreating = true;

  noteForm: FormGroup = new FormGroup({
    text: new FormControl('', {validators: [Validators.required, Validators.maxLength(5000)]}),
  });

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private noteService: NoteService,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.loadNote();
  }

  loadNote() {
    if (this.router.url == "/notes/create") {
      this.isCreating = true;
    } else {
      this.route.params.subscribe(async params => {
        const id = <number>params["id"];
        if (id && id != 0) {
          this.noteService.get(id).pipe().subscribe(u => {
            this.updateForm(u);
          });
        }
      });
    }
  }

  private updateForm(note: INote) {
    this.isCreating = false;

    this.note = note;
    this.noteForm.patchValue({
      text: note.text,
    });
  }

  save() {
    if (this.noteForm.invalid) {
      this.toastrService.error("Form fields are not valid", "Update")
      return;
    }

    const note = deepCopy(this.noteForm.value) as INote;

    const action$ = this.note && this.note.id ? this.noteService.update(this.note.id, note) : this.noteService.create(note)
    action$.subscribe(r => {
      this.toastrService.success(`Note ${this.isCreating ? 'created' : 'updated'} successfully`, "Update")
      this.updateForm(r);
      this.router.navigate(["/notes"])
    })
  }
}
