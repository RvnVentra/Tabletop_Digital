import React, { Component } from 'react';
import './CreateAccount.css';

export class CreateAccount extends Component
{
	constructor(props)
	{
		super(props);
		this.state =
		{
			username: '',
			password: '',
			email: '',
			displayname: '',
			errorMessage: '',
			hidden: true
		}

		this.toggleShow = this.toggleShow.bind(this);
	}

	toggleShow() 
	{
		this.setState({ hidden: !this.state.hidden });
	}

	setValue(e) 
	{
		let id = e.target.id;

		if (id === 'username') 
		{
			this.setState(
			{
				username: e.target.value
			});
		}
		else if (id === 'password') 
		{
			this.setState(
			{
				password: e.target.value
			});
		}
		else if (id === 'email') 
		{
			this.setState(
			{
				email: e.target.value
			});
		}
		else if (id === 'displayname') 
		{
			this.setState(
			{
				displayname: e.target.value
			});
		}
	}

	submit = e =>
	{
		e.preventDefault();

		var rUsername = /^[A-Za-z]{1,}$/;
		var rPassword = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;
		var rEmail = /^[a-zA-Z0-9]{2,}\@[A-Za-z]{1,}\.[A-Za-z]{1,}$/;
		var rDisplayName = /^[A-Za-z0-9]{1,}$/;

		if (this.state.username === null ||
			this.state.username === '' ||
			this.state.password === null ||
			this.state.password === '' ||
			this.state.email === null ||
			this.state.email === '' ||
			this.state.displayname === null ||
			this.state.displayname === '')
		{
			this.setState({ errorMessage: 'Please fill in all the fields' });
		}
		else 
		{
			if (!rUsername.test(this.state.username)) 
			{
				this.setState({ errorMessage: 'Username must not be empty and cannot contain special letters.' });
			}

			if (!rPassword.test(this.state.password)) 
			{
				this.setState({ errorMessage: 'Password must contain 8 characters, at least one letter, one number, and one special character.' });
			}

			if (!rEmail.test(this.state.email)) 
			{
				this.setState({ errorMessage: 'Email must be properly formatted.' });
			}

			if (!rDisplayName.test(this.state.displayname)) 
			{
				this.setState({ errorMessage: 'Display name cannot contain special letters.' });
			}
		}

		this.createAccount();
	}

	createAccount()
	{
		//console.log("TEST");
		//fetch('Main/AddAccount');

		fetch('Main/AddAccount',
			{
				method: 'POST',
				headers:
				{
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(
				{
					username: 'username'
				})
			});
	}


	render() 
	{
		return (
			<div className="accBody">
				<h4 className="accTitle">Account Creation</h4>
				<form className="formBody" method="POST" onSubmit={this.submit.bind(this)}>

					<label className="accLabel">Username: </label>
					<input type="text" className="accInputU" placeholder="Enter a Username"
						id="username" ref="username" onChange={(e) => this.setValue(e)} />
					<br />

					<label className="accLabel">Password: </label>
					<input type={this.state.hidden ? "password" : "text"} className="accInputP"
						placeholder="Enter a Password" id="password" onChange={(e) => this.setValue(e)} />
					<button onClick={this.toggleShow}>Show / Hide</button>
					<br />

					<label className="accLabel">Email: </label>
					<input type="text" className="accInputE" placeholder="example@example.ca"
						id="email" onChange={(e) => this.setValue(e)} />
					<br />

					<label className="accLabel">Display Name: </label>
					<input type="text" className="accInputD" placeholder="Enter a Display Name"
						id="displayname" onChange={(e) => this.setValue(e)} />
					<br />

					<p className="errMessage">{this.state.errorMessage}</p>
					<input className="accSubmit" type="Submit" value="Submit" />
					<input className="accReset" type="Reset" value="Reset" />

				</form>
			</div>);
	}
}

//let regexMessage = this.state.regexMessage.map((item, i) => {
//	return <p key={i}>{item}</p>
//});

//for (let i = this.state.regexMessage.length - 1; i >= 0; i--) {
//	regexMessage.push(<p>{this.regexMessage}</p>);
//}
