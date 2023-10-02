import { Component, OnInit } from '@angular/core';
import { LottoTicketWebApiService } from '../lotto-ticket.web-api.service';
import { ActivatedRoute } from '@angular/router';
import { LoggerService } from '../../global';
import { Subscription } from 'rxjs';
import { LottoTicketDetailBoxModel, LottoTicketDetailModel } from './lotto-ticket-detail.models';

const CLASS = "LottoTicketDetailComponent";
const myModal = document.getElementById('myModal')
const myInput = document.getElementById('myInput')

@Component({
    selector: 'lotto-ticket-detail',
    styleUrls: ['./lotto-ticket-detail.component.scss'],
    templateUrl: 'lotto-ticket-detail.component.html'
})
export class LottoTicketDetailComponent implements OnInit {
    constructor(
        private readonly webApiService: LottoTicketWebApiService,
        private readonly logger: LoggerService,
        private readonly route: ActivatedRoute
    ) {
    }

    public id?: number;
    public model?: LottoTicketDetailModel;
    private subscriptions: Subscription[] = []
    ngOnInit(): void {
        this.subscriptions.push(
            this.route.params.subscribe((params) => {
                this.id = +params['id'];
                this.load();
            })
        )

    }

    get numberPerBox(): number[]{
        let numbers = [];
        for(let number = 1; number <= 49; number++) numbers.push(number);
        return numbers;
    }

    async load() {
        this.logger.logInfo(CLASS + ".load");
        if (!this.id) { this.logger.logInfo(CLASS + ".load / failed"); return; }
        const dto = await this.webApiService.getLottoTicketDetail(this.id);
        this.model = new LottoTicketDetailModel(dto!);
        debugger;
    }

    checkMarked(box: LottoTicketDetailBoxModel, number: number): boolean {
        return box.numbers.some(n => n == number);
    }
}