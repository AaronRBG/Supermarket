import React, { Component } from 'react';

export class Supermarket extends Component {
    static displayName = Supermarket.name;

    constructor(props) {
        super(props);
        this.state = { skus: [], selected_skus: "", price: 0, loading: true };
    }

    componentDidMount() {
        this.populateSupermarket();
    }

    async updatePrice(id) {
        let aux = this.state.selected_skus + id;
        const response = await fetch('Supermarket/Checkout?selected_skus='+aux);
        const data = await response.json();
        this.setState({ selected_skus: aux, price: data });
    }

    render() {
        return (
            <div>
                <h1 id="tabelLabel" >Supermarket</h1>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Unit Price</th>
                            <th>Offer</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.skus.map(sku =>
                            <tr key={sku.id}>
                                <td>{sku.id}</td>
                                <td>{sku.unit_price}</td>
                                <td>{(sku.offer_quantity != 0) ? sku.offer_quantity + "x" + sku.offer_price : ""}</td>
                                <td><button id={sku.id} class="btn btn-primary" onClick={this.updatePrice.bind(this, sku.id)}>Add</button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <div>
                    <div class="left"><p class="inline"><h5>Selected SKUs: {this.state.selected_skus}</h5></p></div>
                    <div class="right"><p class="inline"><h5>Total price: {this.state.price}&euro;</h5></p></div>
                    </div>
            </div>
        );
    }

    async populateSupermarket() {
        const response = await fetch('Supermarket');
        const data = await response.json();
        this.setState({ skus: data, loading: false });
    }
}
