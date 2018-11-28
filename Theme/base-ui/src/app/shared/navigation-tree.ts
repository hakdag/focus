export class NavigationTree {
    public static Tree = [
        {
            id: 1,
            text: '',
            class: 'fa fa-home',
            link: '/app/dashboard',
            parent: null,
            children: [
				{
                    id: 2,
                    text: 'Reserve/Book',
                    class: 'fa fa-table',
                    parent: 1,
					children: [
                        {
                            id: 3,
                            text: 'Check In',
                            link: '/app/booking/checkinlist',
                            class: '',
                            parent: 2,
                            children: [
                                {
                                    id: 4, text: '', class: '', parent: 3, children: []
                                }
                            ]
                        },
                        {
                            id: 11,
                            text: 'Reservation',
                            link: '/app/booking/reservationlist',
                            class: '',
                            parent: 2,
                            children: [
                                {
                                    id: 12, text: '', class: '', parent: 11, children: []
                                }
                            ]
                        },
					]
				},
				{
                    id: 4,
                    text: 'Profile',
                    class: 'fa fa-table',
                    parent: 1,
					children: [
                        {
                            id: 5,
                            text: 'Guest Profile',
                            link: '/app/profile/guestlist',
                            class: '',
                            parent: 4,
                            children: [
                                {
                                    id: 6, text: '', class: '', parent: 5, children: []
                                }
                            ]
                        },
					]
				},
				{
                    id: 6,
                    text: 'Billing',
                    class: 'fa fa-table',
                    parent: 1,
					children: [
                        {
                            id: 7,
                            text: 'Invoice',
                            link: '/app/billing/invoicelist',
                            class: '',
                            parent: 6,
                            children: [
                                {
                                    id: 8, text: '', class: '', parent: 7, children: []
                                }
                            ]
                        },
                        {
                            id: 9,
                            text: 'Invoice Status',
                            link: '/app/billing/invoicestatuseslist',
                            class: '',
                            parent: 6,
                            children: [
                                {
                                    id: 10, text: '', class: '', parent: 9, children: []
                                }
                            ]
                        },
					]
				},
            ]
        }
    ]
}
