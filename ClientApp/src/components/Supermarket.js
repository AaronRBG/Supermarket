import React, { Component } from 'react';

export class Supermarket extends Component {
    static displayName = Supermarket.name;

    constructor(props) {
        super(props);
        this.state = { skus: [], loading: true };
    }

    componentDidMount() {
        this.populateSupermarket();
    }

    static renderSkusTable(skus) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Unit Price</th>
                        <th>Offer Quantity</th>
                        <th>Offer Price</th>
                    </tr>
                </thead>
                <tbody>
                    {skus.map(sku =>
                        <tr key={sku.id}>
                            <td>{sku.id}</td>
                            <td>{sku.unit_price}</td>
                            <td>{sku.offer_quantity}</td>
                            <td>{sku.offer_price}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Supermarket.renderSkusTable(this.state.skus);

        return (
            <div>
                <h1 id="tabelLabel" >Supermarket</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateSupermarket() {
        const response = await fetch('Supermarket');
        const data = await response.json();
        this.setState({ skus: data, loading: false });
    }
}
