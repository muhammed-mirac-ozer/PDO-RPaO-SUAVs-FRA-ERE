using System;

class LineIntersection
{
    static void Main()
    {
        // Input: Coordinates of the given line segment
        double x0_0 = 1.0; // replace with actual values
        double y0_0 = 2.0;
        double x0_1 = 5.0;
        double y0_1 = 6.0;

        // Input: Number of line segments formed by the boundaries of the obstacles (n)
        int n = 3; // replace with actual value

        // Input: Coordinates of other line segments
        double[,] xi_0 = { { 2.0, 3.0 }, { 3.0, 4.0 }, { 4.0, 5.0 } }; // replace with actual values
        double[,] yi_0 = { { 3.0, 4.0 }, { 4.0, 5.0 }, { 5.0, 6.0 } };

        // Vector consisting of measured intersection points
        int[] L = new int[n];

        // Step 1: Check parallel lines and remove them
        double l0 = (y0_1 - y0_0) / (x0_1 - x0_0);

        for (int i = 0; i < n; i++)
        {
            double li = (yi_0[i, 1] - xi_0[i, 0]) / (xi_0[i, 1] - xi_0[i, 0]);

            if (Math.Abs(l0 - li) < double.Epsilon) // Check for parallel lines using epsilon
            {
                // Remove parallel line
                n--;
                i--; // Adjust index after removing an element
            }
        }

        // Step 2: Calculate distances and check for intersections
        double j0 = Math.Sqrt(Math.Pow(x0_1 - x0_0, 2) + Math.Pow(y0_1 - y0_0, 2));

        int m = 1;

        for (int i = 0; i < n; i++)
        {
            double xi_1 = xi_0[i, 1];
            double yi_1 = yi_0[i, 1];

            double ji = Math.Sqrt(Math.Pow(xi_1 - xi_0[i, 0], 2) + Math.Pow(yi_1 - yi_0[i, 0], 2));
            double cosTheta = ((x0_1 - x0_0) * (xi_1 - xi_0[i, 0]) + (y0_1 - y0_0) * (yi_1 - yi_0[i, 0])) / (j0 * ji);
            double sinTheta = Math.Sqrt(1 - Math.Pow(cosTheta, 2));
            double d1 = 0.5 * j0 * ji * sinTheta;

            // Calculate lengths
            double xi_x0 = xi_0[i, 0] - x0_0;
            double xi_y0 = yi_0[i, 0] - y0_0;
            double xi_x1 = xi_1 - x0_0;
            double xi_y1 = yi_1 - y0_0;

            double xi_x0_len = Math.Sqrt(Math.Pow(xi_x0, 2) + Math.Pow(xi_y0, 2));
            double xi_y0_len = Math.Sqrt(Math.Pow(xi_x1, 2) + Math.Pow(xi_y1, 2));
            double yi_x0_len = Math.Sqrt(Math.Pow(x0_1 - xi_0[i, 0], 2) + Math.Pow(y0_1 - yi_0[i, 0], 2));
            double yi_y0_len = Math.Sqrt(Math.Pow(x0_1 - xi_1, 2) + Math.Pow(y0_1 - yi_1, 2));

            double d_X0_Xi_Y0 = 0.5 * xi_x0_len * xi_y0_len * Math.Sin(Math.Asin(xi_x0 / xi_x0_len) - Math.Asin(xi_y0 / xi_y0_len));
            double d_X0_Yi_Y0 = 0.5 * yi_x0_len * yi_y0_len * Math.Sin(Math.Asin((x0_1 - xi_1) / yi_x0_len) - Math.Asin((y0_1 - yi_1) / yi_y0_len));

            double d_i = d_X0_Xi_Y0 + d_X0_Yi_Y0;

            // Intersection condition
            if (Math.Abs(d_i - d1) < double.Epsilon) // Check for equality using epsilon
            {
                L[m] = i;
                m++;
            }
        }

        // Step 3: Print intersection points
        if (m > 1)
        {
            Console.WriteLine("Intersection Points:");
            for (int i = 1; i < m; i++)
            {
                Console.WriteLine($"Point {i}: ({xi_0[L[i], 0]}, {yi_0[L[i], 0]})");
            }
        }
        else
        {
            Console.WriteLine("No intersection points found.");
        }
    }
}
