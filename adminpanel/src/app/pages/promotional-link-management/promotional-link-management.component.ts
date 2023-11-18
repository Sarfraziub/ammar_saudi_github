import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
  selector: 'promotional-link-management',
  templateUrl: './promotional-link-management.html'
})
export class PromotionalLinkManagementComponent extends BaseComponent {

  isFullScreen: boolean

  constructor() { 
    super()
  }

  ngOnInit(): void {
  }

}
