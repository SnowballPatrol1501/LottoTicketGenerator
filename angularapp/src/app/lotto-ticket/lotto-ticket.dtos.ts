export * from './lotto-ticket-detail/lotto-ticket-detail.dtos'

export class LottoTicketListResult {
    public id!: number;
    public hasSuperNumber!: boolean;
    public countOfBoxes!: number;
}