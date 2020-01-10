# AmazonSim
This is a 3D warehouse simulator created by second year software engeneering students at NHL Stenden Hogeschool.
The warehouse we are simulating is located on the moon and is inspired by Amazon warehouses, where robots move crates around to a desired location in the warehouse.
The game makes use of ASP.Net and WebGL and requires IIS Express to run.

We tried to randomize as much as we could to make this a true simulation.
Everything from the spawn time and load weight of the inbound and outbound export vehicles, to the weight and contents of the item crates is randomized.
The robots make use of the Dijkstra path finding algorithm and must decide which crates to load on the export vehicle.
