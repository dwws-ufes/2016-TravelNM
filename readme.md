<div style="color: black; font-size:14; font-family: arial, verdana">

<b>TRAVEL MN</b>

<br>

<p>Assignment for the 2016 edition of the "Web Development and the Semantic Web" course, by Nilber Vittorazzi de Almeida and Marcio Freitas.</p>

<p>To run this project follow the steps below:</p>

<p>1 - Download Visual Studio 2015 Community  <a href="https://www.visualstudio.com/pt-br/products/visual-studio-community-vs.aspx" target="blank"> https://www.visualstudio.com/pt-br/products/visual-studio-community-vs.aspx</a>;</p>

<p>2 - The IDE Visual Studio have a integration with Github, for this click on menu item View -> Team Explorer. Then do the project clone for the IDE;</p>

<p>3 - After downloading the project, click the newly created project;</p>

<p>4 - In Travel MN project properties alter target framework for 4.5, because VS 2015.</p>

<p>5 - When you run the application, all libraries will be automatically downloaded.</p>

<p>6 - The database used is SQLite 3. We need to change the path of the database on the web config file, like this:</p>

<blockquote>
<xmp style="color: blue; font-size:14">
	<connectionStrings>
		<add name="TravelMNContext" connectionString="Data Source=Path\TravelMN.sqlite" providerName="System.Data.SQLite"/>
	</connectionStrings>
</xmp>
</blockquote>

<br>

<p><b>Features:</b></p>

<p>This system is divided into two parts: Administrative and Customer Area: </p>

<p> - In the Administrative Area are possible CRUD operations in Users, Cities, Customers, Travel Packages and Sale Packages;</p>

 <p>- In the Customer Area, the customer can Buy Packages, See Bought Packages and alter Personal Data.</p>

<br>
 
<p><b>Other Features:</b></p>

 <p>- Entity Framework for mapping object/ relational;</p>
 <p>- Nuget Dependecy Manager;</p>
 <p>- Ninject for Dependecy Injection;</p>
 <p>- ASP.Net MVC and Razor Decorator.</p>
 
 <br>

<p><b>Highlights:</b></p>
 
  <p>- Responsive Layout;</p>
  <p>- Multiple Language Support, in this case en-US and pt-BR, but others resources can be created;</p>
  <p>- HTML5 Validations;</p>
  <p>- Login with Salt Encryption MD5, for Customers;</p>
  <p>- Jquery Autocomplete;</p>
  <p>- Jquery Datatables;</p>
  <p>- Custom error page for login;</p>
  <p>- System in Modules: Application, Interfaces, Model, Persistence and TravelMN Web.</p>

  <br>

<p><b>Users:</b></p>

<p>Administrative Area:</p>

<p>User: admin@travelmn.com.br - Password: 123</p>

<p>Customer Area: </p>

<p>User: customer@travelmn.com.br - Password: 1234567</p>

</div>




