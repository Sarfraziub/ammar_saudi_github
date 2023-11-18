import { ClientOrderStatus } from '../enum';


export class ClientORderStatusLookup {
    static get getLookup() {
        return [
            { text: 'جديد', id: ClientOrderStatus.New },
            { text: 'تم استلام الدفعة', id: ClientOrderStatus.PaymentReceived },
            { text: 'مع سائق', id: ClientOrderStatus.WithDriver },
            { text: 'تم التوصيل', id: ClientOrderStatus.Delivered },
        ];
    }

    static getById(id) {
        return this.getLookup.find(f => f.id === id)?.text;
    }
}
