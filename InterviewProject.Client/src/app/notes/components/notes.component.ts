import {Component} from "@angular/core";
import {ConfirmDialogModel} from "../../features/confirm-dialog/classes/confirm-dialog-model";
import {ConfirmDialogComponent} from "../../features/confirm-dialog/components/confirm-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {IFilterCommand} from "../../features/filter/interfaces/filter-command";
import {map} from "rxjs";
import {PageEvent} from "@angular/material/paginator";
import {NoteService} from "../services/note.service";
import {ToastrService} from "ngx-toastr";
import {INote} from "../interfaces/note";

@Component({
  selector: "notes",
  templateUrl: "./notes.component.html",
  styleUrls: ["./notes.component.scss"]
})
export class NotesComponent {

  public notes: INote[] = [];
  public totalNotes: number = 0;

  public page = 1;
  public rowsPage = 5;

  searchValue: string = '';

  displayedColumns: string[] = ['id', 'text', 'actions'];
  constructor(
    public dialog: MatDialog,
    private readonly noteService: NoteService,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.getRemoteData();
  }

  getRemoteData() {
    const command = <IFilterCommand> {
      skip: (this.page - 1) * this.rowsPage,
      take: this.rowsPage,
      searchQuery: this.searchValue
    }

    this.noteService.getPage(command).pipe(
      map(r => {
        this.totalNotes = r.total
        return r.items;
      })
    ).subscribe(u => {
      this.notes = u;
    })
  }

  onPageChanged(event: PageEvent) {
    this.page = event.pageIndex + 1;

    this.getRemoteData();
  }

  executeFilter() {
    this.page = 1;

    this.getRemoteData();
  }

  onDelete(selectedUser: any) {
    const dialogData = new ConfirmDialogModel("Confirm Action", `Are you sure you want to remove this note?`);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.noteService.delete(selectedUser.id).subscribe(r => {
          if(r) {
            this.executeFilter();
            this.toastrService.success("Note removed successfully", "Remove")
          }
        })
      }
    });
  }
}
