import { Component, Input } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
  selector: 'order-management-view-feedback-cmp',
  templateUrl: './order-management-view-feedback.html',
})
export class orderManagementViewFeedbackComponent extends BaseComponent {

  private _selectedItem: any = null;
  feedBack: string = '';
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    if (item) {
      this.feedBack = item.feedback;
    } else {
      this.feedBack = '';
    }
  }

  constructor() {
    super();
  }

}