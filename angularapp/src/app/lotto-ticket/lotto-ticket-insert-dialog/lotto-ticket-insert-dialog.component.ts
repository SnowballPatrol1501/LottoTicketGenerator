import { Component, ElementRef, EventEmitter, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'lotto-ticket-insert-dialog',
    template: `
        <ng-template #contentModal class="modal-dialog" let-c="close" let-d="dismiss">
            <div class="modal-content">
              <div class="modal-header">
                <h1 class="modal-title fs-5" id="exampleModalLabel">Ticket hinzufügen</h1>
                <button type="button" class="btn-close" (click)="close()"></button>
              </div>
              <div class="modal-body">
                <form>
                    <div class="form-group">
                        <label for="boxAmount">Anzahl der Boxen</label>
                        <input type="number" class="form-control" placeholder="Anzahl der Boxen" [(ngModel)]="dialogModel.amountOfBoxes" name="dialogModel.amountOfBoxes">
                    </div>
                    <div class="form-group form-check">
                        <label class="form-check-label">Superzahl</label>
                        <input type="checkbox" [(ngModel)]="dialogModel.generateSuperNumber" name="dialogModel.generateSuperNumber" class="form-check-input">
                    </div>
                </form>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" (click)="close()">Close</button>
                <button type="button" class="btn btn-primary" (click)="save()">Draw Ticket</button>
              </div>
            </div>
        </ng-template>
    `
})
export class LottoTicketInsertDialogComponent {
    constructor(
        private readonly modalService: NgbModal
    ) {
    }

    @ViewChild('contentModal') test!: ElementRef;
    savedEmitter = new EventEmitter();
    modal?: NgbModalRef;
    dialogModel: LottoTicketInsertDialogResult = new LottoTicketInsertDialogResult(0, false);

    show(): EventEmitter<LottoTicketInsertDialogResult> {
        this.modal = this.modalService.open(this.test);
        return this.savedEmitter;
    }

    save() {
        this.savedEmitter.emit(this.dialogModel);
        this.modal!.close();
    }

    close() {
        this.modal!.close();
    }
}

export class LottoTicketInsertDialogResult {
    constructor(
        public amountOfBoxes: number,
        public generateSuperNumber: boolean
    ) { }
}