using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            BonusOne();
            //BonusTwo();
            
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count

            var users = _context.Users;
            var userCount = users.ToList().Count();

            Console.WriteLine(userCount);
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            var product_name = _context.Products;
            var product = product_name.Where(price => price.Price > 150);


            foreach (var item in product)
                Console.WriteLine(item.Name + " $" + item.Price);
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.

        }

        private void ProblemFour()
        {
            var product_name = _context.Products;
            var product = product_name.Where(name => name.Name.Contains("s"));

            foreach (var item in product)
            {
                Console.WriteLine(item.Name);
            }
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

        }

        private void ProblemFive()
        {
            var user_name = _context.Users;
            DateTime oldDate = new DateTime(2016, 1, 1);
            var user = user_name.Where(date => date.RegistrationDate < oldDate);

            foreach (var person in user)
            {
                Console.WriteLine(person.Email + " " + person.RegistrationDate);
            }

            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.

            var user_name = _context.Users;
            DateTime oldDate = new DateTime(2016, 1, 1);
            DateTime otherDate = new DateTime(2018, 1, 1);
            var user = user_name.Where(date => date.RegistrationDate < otherDate && date.RegistrationDate > oldDate);

            foreach (var person in user)
            {
                Console.WriteLine(person.Email + " " + person.RegistrationDate);
            }
        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var customerUser = _context.ShoppingCarts.Include(ur => ur.User).Include(p => p.Product).Where(ur => ur.User.Email == "afton@gmail.com");
            foreach (var product in customerUser)
            {
                Console.WriteLine("Product Name: " + product.Product.Name + " $" + product.Product.Price + " QTY: " + product.Quantity);
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            decimal customerUser = _context.ShoppingCarts.Include(ur => ur.User).Include(p => p.Product).Where(ur => ur.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();

            Console.WriteLine(customerUser);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var employeeUser = _context.UserRoles.Where(ur => ur.RoleId == 2).Select(ur => ur.UserId);
            var shoppingCart = _context.ShoppingCarts.Include(b => b.User).Include(p => p.Product).Where(ur => employeeUser.Contains(ur.UserId));

            foreach (var item in shoppingCart)
                Console.WriteLine("User Email: " + item.User.Email + " " + "Product Name: " + item.Product.Name + " $" + item.Product.Price + " QTY: " + item.Quantity);

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "jimbob13@gmail.com",
                Password = "JimmysPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.

            Product newProduct = new Product()
            {
                Name = "DevCodeCamp",
                Description = "Learn to Code In 12 Easy Weeks",
                Price = 9001
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "jimbob13@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var shopper = _context.Users.Where(u => u.Id == 6).Select(u => u.Id).SingleOrDefault();
            var product = _context.Products.Where(p => p.Name == "DevCodeCamp").Select(p => p.Id).SingleOrDefault();
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                UserId = shopper,
                ProductId = product,
            };
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "jimbob13@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(u => u.Price == 9001).SingleOrDefault();
            product.Price = 9999;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Enter email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            var password = Console.ReadLine();
            var users = _context.Users.ToList();
            //var usersPassword = _context.Users;
            foreach (var user in users)
            {
                bool valid = false;
                while (valid == false)
                {

                    if (user.Email == email && user.Password == password)
                    {
                        Console.WriteLine("Signed In!");
                        valid = true;
                    }

                    else if (user.Email != email && user.Password != password)
                    {
                        Console.WriteLine("Enter email: ");
                        email = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        password = Console.ReadLine();
                        valid = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Email or Password");
                        break;
                    }



                }
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.

            var customers = _context.Users.ToList();
            decimal combinedTotal = 0;
            
            foreach (var user in customers)
            {
                //exception on line below
                var userTotal = _context.ShoppingCarts.Include(ur => ur.User).Include(p => p.Product).Where(e => e.User.Id == user.Id).Select(t => t.Product.Price * t.Quantity).Sum();
                Console.WriteLine($"Email: {user.Email} Total: ${userTotal}");
                combinedTotal += userTotal.Value;
            }
            Console.WriteLine($"Combined Total: ${combinedTotal}");
        }
    
        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

        }

    }
}
