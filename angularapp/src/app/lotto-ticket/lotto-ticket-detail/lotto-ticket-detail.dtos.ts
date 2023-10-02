export class LottoTicketDetailResult {
    id!: number;
    superNumber?: number | null;
    showSuperNumber!: boolean;
    ticketBoxes!: LottoTicketDetailBoxResult[];
}

export class LottoTicketDetailBoxResult {
    id!: number;
    numbers!: number[];
}