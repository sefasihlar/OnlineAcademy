namespace NLayer.Academy.Middlewares
{
    public class UseCustomExeptionHandler
    {
        //public static void UserCustomException(this IApplicationBuilder app)
        //{
        //    app.UseExceptionHandler(configure =>
        //    {
        //        configure.Run(async context =>
        //        {


        //            context.Response.ContentType = "/application/json";

        //            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

        //            var StatusCode = exceptionFeature.Error switch
        //            {
        //                ClientSideException => 400,
        //                _ => 500
        //            };

        //            context.Response.StatusCode = StatusCode;

        //            var response = CustomResponseDto<NoContentDto>.Fail(StatusCode, exceptionFeature.Error.Message);


        //            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        //        });


        //    });



        //}
    }
}
