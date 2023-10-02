import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CreateLottoTicketCommand, LottoTicketWebApiService } from './lotto-ticket.web-api.service';
import { LoggerService } from '../global';
import { LottoTicketListModel } from './lotto-ticket.models';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LottoTicketInsertDialogComponent, LottoTicketInsertDialogResult } from './lotto-ticket-insert-dialog/lotto-ticket-insert-dialog.component';

const CLASS = "LottoTicketComponent";


@Component({
    selector: 'lotto-tickets',
    templateUrl: 'lotto-ticket.component.html'
})
export class LottoTicketComponent implements OnInit {
    constructor(
        private readonly modalService: NgbModal,
        private readonly webApiService: LottoTicketWebApiService,
        private readonly logger: LoggerService,
        private readonly router: Router,
    ) {
    }
    ngOnInit(): void {
        this.load();
    }

    @ViewChild('insertDialog') insertDialog!: LottoTicketInsertDialogComponent;
    models: LottoTicketListModel[] = [];
    async load() {
        this.logger.logInfo(CLASS + ".load");
        try {
            const dtos = await this.webApiService.getLottoTickets();
            this.models = dtos!.map(d => new LottoTicketListModel(d));
        } catch (error) {
            this.logger.logError(error);
        }
    }

    selectModel(model: LottoTicketListModel) {
        this.logger.logInfo(CLASS + ".selectModel");
        this.router.navigate([`/lotto-ticket/ticket/${model.id}`]); 
    }

    public createTicketModal() {
        this.logger.logInfo(CLASS + ".createTicketModal");
        this.insertDialog!.show().subscribe(async (result) => await this.insertTicket(result));
    }

    private async insertTicket(result: LottoTicketInsertDialogResult) {
        this.logger.logInfo(CLASS + ".insertTicket");
        try {
            const cmd = new CreateLottoTicketCommand(result.amountOfBoxes, result.generateSuperNumber);
            const id = await this.webApiService.createLottoTicket(cmd);
            this.router.navigate([`/lotto-ticket/ticket/${id}`]);
        } catch (error) {
            this.logger.logError(error);
        }
    }
}