import { Injectable } from "@angular/core";
import { WebApiServiceBase } from "../web-api/web-api-base.service";
import { HttpClient } from "@angular/common/http";
import * as Dtos from "./lotto-ticket.dtos";

@Injectable({ providedIn: 'root' })
export class LottoTicketWebApiService extends WebApiServiceBase {
    constructor(httpClient: HttpClient){
        super(httpClient, 'LottoTicket');
    }

    getLottoTickets(){
        return this.get<Dtos.LottoTicketListResult[]>('GetLottoTickets');
    }

    getLottoTicketDetail(id: number){
        return this.get<Dtos.LottoTicketDetailResult>('GetLottoTicketDetail', { id });
    }

    createLottoTicket(param: CreateLottoTicketCommand){
        return this.post<number>('CreateLottoTicket', param, true);
    }
}

export class CreateLottoTicketCommand{
    constructor(
        public NumOfBoxes: number,
        public GenerateSuperNumber: boolean
    ){}
}