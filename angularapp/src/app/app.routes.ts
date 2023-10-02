import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    { path: '', redirectTo: 'lotto-ticket/tickets', pathMatch: 'full' },
]

export const AppRouting = RouterModule.forRoot(routes)