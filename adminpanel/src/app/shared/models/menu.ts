
export class Menu {
    static systemDefinition = {
        id:1,
        title: 'Definitions',        
        rootIcon: 'fal fa-cogs',
        isBase: true,
    }
    static orderManagement = {
        id:2,
        title: 'Order Management',        
        rootIcon: 'fal fa-users-medical',
        isBase: true,
    }
    static patientBillingServices = {
        id:3,
        title: 'Patient Billing Services',
        rootIcon: 'fal fa-business-time',
        isBase: true,
    }

    static patientManagement = {
        id:4,
        title: 'Patient Management',
        rootIcon: 'fal fa-users-medical',
        isBase: true,
    }
    
    static priceManagement = {
        id:5,
        title: 'Price Management',
        rootIcon: 'fal fa-money-bill-alt',
        isBase: true,
    }

    static claimManagement = {
        id:6,
        title: 'Claim Management',
        rootIcon: 'fal fa-laptop-medical',
        isBase: true,
    }

    static invoicemManagement = {
        id:7,
        title: 'Invoice Management',
        rootIcon: 'fal fa-file-invoice-dollar',
        isBase: true,
    }
}