import { LottoTicketListResult } from "./lotto-ticket.dtos";

export class LottoTicketListModel extends LottoTicketListResult {
    constructor(dto: LottoTicketListResult) {
        super();
        return {
            ...dto
        };
    }
}