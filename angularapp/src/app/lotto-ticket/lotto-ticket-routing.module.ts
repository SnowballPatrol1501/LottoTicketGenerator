import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LottoTicketComponent } from './lotto-ticket.component';
import { LottoTicketDetailComponent } from './lotto-ticket-detail/lotto-ticket-detail.component';

const routes: Routes = [
    { path: 'lotto-ticket/ticket/:id', component: LottoTicketDetailComponent },
    { path: 'lotto-ticket/tickets', component: LottoTicketComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class LottoTicketRouterModule{
    
}
