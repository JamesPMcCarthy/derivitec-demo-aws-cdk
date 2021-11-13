using System;

using Amazon.CDK;
namespace aws_cdk
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var app = new App();

                string AWS_Account = System.Environment.GetEnvironmentVariable("aws-account-key") ?? throw new ArgumentException("ERR# - Aws Account Number must be provided!");

                // Stack must be in us-east-1, because the ACM certificate for a
                // global CloudFront distribution must be requested in us-east-1.
                new StaticSiteStack(app, "StaticSiteStackCDK", new StackProps
                {
                    Env = new Amazon.CDK.Environment
                    {
                        Account = AWS_Account,
                        Region = "us-east-1"
                    }
                });

                app.Synth();
            }
            catch (Exception e){
                Console.WriteLine(e);
            }
             

        }
    }
}
