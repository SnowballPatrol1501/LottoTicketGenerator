import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LottoTicketWebApiService } from './lotto-ticket.web-api.service';
import { LottoTicketComponent } from './lotto-ticket.component';
import { LottoTicketRouterModule } from './lotto-ticket-routing.module';
import { LottoTicketDetailComponent } from './lotto-ticket-detail/lotto-ticket-detail.component';
import { LottoTicketInsertDialogComponent } from './lotto-ticket-insert-dialog/lotto-ticket-insert-dialog.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    LottoTicketRouterModule,
  ],
  declarations: [
    LottoTicketComponent,
    LottoTicketDetailComponent,
    LottoTicketInsertDialogComponent
  ],
  providers:[
    NgbActiveModal,
    LottoTicketWebApiService
  ]
})
export class LottoTicketModule { }
