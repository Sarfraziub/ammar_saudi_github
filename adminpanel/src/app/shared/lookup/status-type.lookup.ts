import { StatusType } from '../enum';


export class StatusTypeLookup {
    static get getLookup() {
        return [
            { text: 'جديد', id: StatusType.New },
            { text: 'قيد التوصيل', id: StatusType.InProgress },
            { text: 'تم التسليم', id: StatusType.Completed },
        ];
    }
}
